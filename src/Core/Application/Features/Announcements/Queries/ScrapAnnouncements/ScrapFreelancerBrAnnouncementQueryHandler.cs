using System.Text.RegularExpressions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;
using AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Shared;
using MediatR;

public class ScrapFreelancerBrAnnouncementQueryHandler : IRequestHandler<ScrapFreelancerBrAnnouncementQuery, Either<BaseException, Announcement>>
{
  private readonly HeadlessProvider _headlessProvider;

  public ScrapFreelancerBrAnnouncementQueryHandler(HeadlessProvider headlessProvider)
  {
    _headlessProvider = headlessProvider;
  }

  public async Task<Either<BaseException, Announcement>> Handle(ScrapFreelancerBrAnnouncementQuery request, CancellationToken cancellationToken)
  {
    var container = "#generalApp > main > div > div.project-info > div";

    await _headlessProvider.Open(new Open { });

    await _headlessProvider.GoTo(new GoTo
    {
      url = request.url
    });


    bool pageNotFound = await _headlessProvider.ElementExists(new GetElement
    {
      target = "body > div.error-page",
      maxSelectorTimeout = 1000
    });


    if (pageNotFound)
    {
      return new InvalidException("Announcement page doesn't exists");
    }

    var titleExists = await _headlessProvider.GetText(new GetText
    {
      target = $"{container} > h1",
      maxSelectorTimeout = 2000
    });

    if (titleExists.IsLeft())
    {
      return titleExists.left;
    }


    var descriptionExists = await _headlessProvider.GetText(new GetText
    {
      target = $"{container} div > p",
      maxSelectorTimeout = 2000
    });

    if (descriptionExists.IsLeft())
    {
      return descriptionExists.left;
    }

    var authorNameExists = await _headlessProvider.GetText(new GetText
    {

      target = $"{container} > ul > li:nth-child(8) > span.text",
      maxSelectorTimeout = 2000

    });

    if (authorNameExists.IsLeft())
    {
      return authorNameExists.left;
    }


    var statusExists = await _headlessProvider.GetAttribute(new GetAttribute
    {
      target = $"{container} div",
      name = "class",
      maxSelectorTimeout = 2000
    });

    if (statusExists.IsLeft())
    {
      return statusExists.left;
    }

    var tagsExists = await _headlessProvider.GetText(new GetText
    {
      target = $"{container} > p",
      maxSelectorTimeout = 2000
    });

    if (tagsExists.IsLeft())
    {
      return tagsExists.left;
    }

    var publishedAtExists = await _headlessProvider.GetAttribute(new GetAttribute
    {
      target = $"{container} > ul > li:nth-child(3) > span.text > time",
      name = "datetime"
    });

    if (publishedAtExists.IsLeft())
    {
      return publishedAtExists.left;
    }


    await _headlessProvider.Close();


    return new Announcement(titleExists.right,
      descriptionExists.right,
      Regex.IsMatch(statusExists.right, @"open") ? AnnouncementStatus.Open : AnnouncementStatus.Closed,
      tagsExists.right.Split(@",", StringSplitOptions.RemoveEmptyEntries).ToList(),
      request.url,
      new Author(authorNameExists.right, "-"),
      request.platform,
      DateTime.Parse(publishedAtExists.right).ToUniversalTime()
    );
  }

}