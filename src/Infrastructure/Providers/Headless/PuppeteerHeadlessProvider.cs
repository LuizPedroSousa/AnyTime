using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;
using AnyTime.Core.Application.Models.Headless;
using AnyTime.Core.Domain.Shared;
using Newtonsoft.Json.Linq;
using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace AnyTime.Infrastructure.Providers.Headless;


public class PuppeteerHeadlessProvider : HeadlessProvider
{
  private static LaunchOptions getLaunchOptions(bool headless, ViewPortOptions? viewPort = default) => new LaunchOptions()
  {
    Headless = headless,
    Args = new[] { "--load-extensions", "--start-maximized" },
    IgnoredDefaultArgs = new[] {
        "--disable-extensions",
        "--enable-automation",
        "--disable-web-security",
        "--disable-features=IsolateOrigins,site-per-process",
    },
    DefaultViewport = {
       Width = viewPort is not null ? viewPort.Width : 1366,
       Height = viewPort is not null ? viewPort.Width :  768,
    },
  };


  private IPage pageInstance { get; set; }
  private IBrowser browserInstance { get; set; }


  #region Page commands and queries
  public async Task Open(Open dto)
  {


    if (browserInstance is null)
    {
      using var browserFetch = new BrowserFetcher();
      await browserFetch.DownloadAsync();
      var browser = await Puppeteer.LaunchAsync(getLaunchOptions(headless: dto.headless));
      browserInstance = browser;
    }


    pageInstance = await browserInstance.NewPageAsync();

    await pageInstance.SetUserAgentAsync("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36");
    await pageInstance.SetExtraHttpHeadersAsync(
      new Dictionary<string, string>() {
        {"accept-language", "en-US,en;q=0.9,hy;q=0.8"}
      }
    );
  }

  private void Reset()
  {
    this.pageInstance = null;
  }

  public async Task Close()
  {
    await this.pageInstance.CloseAsync();
    Reset();
  }

  public async Task GoTo(GoTo dto)
  {
    var page = pageInstance;

    if (dto.timeout != null) await page.WaitForTimeoutAsync((int)dto.timeout);
    await pageInstance.GoToAsync(dto.url, new NavigationOptions
    {
      WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded },
      Timeout = dto.navigation_timeout
    });
  }


  public async Task Reload()
  {
    await pageInstance.ReloadAsync();
  }

  #endregion Page commands and queries


  #region Keyboard
  public async Task FillInput(FillInput dto)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (dto.timeout != null) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }

      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto.checker);

      if (dto?.isIframe == true)
      {
        await frame.ClickAsync(dto.target, new ClickOptions
        {
          ClickCount = 3
        });
        await frame.TypeAsync(dto.target, dto.content);
      }
      else
      {
        await page.TypeAsync(dto.target, dto.content);
      }
    }
    catch (Exception exception)
    {
    }


  }
  #endregion

  #region Mouse events
  public async Task Click(Click dto)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (dto.timeout != null) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
      {
        Timeout = dto.timeout
      });

      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }


      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto?.checker);

      if (dto?.isIframe == true)
      {
        await frame.ClickAsync(dto.target);
      }
      else
      {
        await page.ClickAsync(dto.target);
      }


    }
    catch (Exception exception)
    {
    }

  }

  #endregion

  #region Selectors
  public async Task<Either<BaseException, Element>> GetElement(GetElement dto)
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (dto.timeout != null) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
      {
        Timeout = dto.maxSelectorTimeout,
      });

      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
      }

      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto?.checker);

      IElementHandle? element;

      if (dto?.isIframe == true)
      {
        element = await frame.QuerySelectorAsync(dto.target);

      }
      else
      {
        element = await page.QuerySelectorAsync(dto.target);
      }

      if (element is null)
      {
        return new NotFoundException("Element was not found");
      }


      return new Element
      {
        textContent = await element.EvaluateFunctionAsync<string>("element => element.textContent")

      };

    }
    catch (Exception exception)

    {
      return new InvalidException("Can't get element");
    }
  }
  public async Task<Either<BaseException, string>> GetText(GetText dto)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (dto.timeout is int) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.maxSelectorTimeout,
        });
      }
      else
      {
        await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.maxSelectorTimeout
        });
      }



      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto?.checker);

      string text;

      if (dto?.isIframe == true)
      {
        text = await frame.QuerySelectorAsync(dto.target).EvaluateFunctionAsync<string>("element => element.textContent");
      }
      else
      {
        text = await page.QuerySelectorAsync(dto.target)
          .EvaluateFunctionAsync<string>("element => element.textContent");
      }
      return text;
    }
    catch (Exception exception)
    {
      return new NotFoundException("Text was not found");
    }
  }

  public async Task<Either<BaseException, string>> GetAttribute(GetAttribute dto)
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (dto.timeout is int) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
        {
          Timeout = dto.timeout
        });
      }

      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto?.checker);

      string attribute;

      if (dto?.isIframe == true)
      {
        attribute = await frame.QuerySelectorAsync(dto.target)
          .EvaluateFunctionAsync<string>($"element => element.getAttribute('{dto.name}')");
      }
      else
      {
        attribute = await page.QuerySelectorAsync(dto.target)
          .EvaluateFunctionAsync<string>($"element => element.getAttribute('{dto.name}')");
      }
      return attribute;
    }
    catch (Exception exception)
    {
      return new InvalidException($"Can't get attribute - {dto.name}");
    }

  }

  public async Task<IEnumerable<T>> GetAllByFunctionEvaluation<T>(GetAllByFunctionEvaluation dto) where T : class
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (dto.timeout != null) await page.WaitForTimeoutAsync((int)dto.timeout);

    try
    {
      await page.WaitForSelectorAsync(dto.target, new WaitForSelectorOptions
      {
        Timeout = dto.timeout
      });


      if (dto.isIframe)
      {
        frame = await GetFrame(dto.maxSelectorTimeout);
      }

      await this.HandleCheckers(dto.target, dto.isIframe, frame, page, dto?.checker);

      JToken elements;


      if (dto?.isIframe == true)
      {
        elements = await frame.EvaluateFunctionAsync(dto.script);
      }
      else
      {
        elements = await page.EvaluateFunctionAsync(dto.script, dto.scriptParams);
      }

      return elements.Select(element => element.ToObject<T>());
    }
    catch (Exception exception)
    {
      return default(IEnumerable<T>);
    }
  }

  #endregion


  private async Task<IFrame> GetFrame(int maxSelectorTimeout)

  {
    var page = pageInstance;

    string iframeSelector = "iframe[id=\"contentFrame\"]";
    var waitForOptions =
            new WaitForSelectorOptions
            {
              Timeout = maxSelectorTimeout
            };

    await page.WaitForSelectorAsync(
       iframeSelector,
       waitForOptions
  );

    var iframeHandle = await page.QuerySelectorAsync(iframeSelector);

    var iframe = await iframeHandle.ContentFrameAsync();

    await iframe.WaitForSelectorAsync(
      "iframe",
      waitForOptions
  );

    var nestedIframeHandle = await iframe.QuerySelectorAsync("iframe");

    return await nestedIframeHandle.ContentFrameAsync();

  }


  #region Validations
  private async Task<bool> HandleCheckers(string target, bool isIframe, IFrame frame, IPage page, ElementChecker? checker)
  {
    if (!string.IsNullOrEmpty(checker?.text))
    {

      string text;
      var selector = @$"() => document.querySelector('{target}').textContent";

      if (isIframe)
      {

        text = await frame.EvaluateFunctionAsync<string>(selector);
      }
      else
      {
        text = await page.EvaluateFunctionAsync<string>(selector);
      }

      if (!string.Equals(checker.text, text))
      {
        return false;
      }

    }

    return true;
  }

  public async Task<bool> ElementExists(GetElement dto)
  {
    var elementExists = await this.GetElement(dto);

    if (elementExists.IsLeft())
    {
      return false;
    }

    return true;
  }
  #endregion
}
