export { render };
export const passToClient = ['pageProps', 'urlPathname'];

import ReactDOMServer from 'react-dom/server';
import { dangerouslySkipEscape, escapeInject } from 'vite-plugin-ssr/server';
import logoUrl from './logo.svg';
import { PageContextServer } from './types';

async function render(pageContext: PageContextServer) {
    const { Page, pageProps } = pageContext;
    // This render() hook only supports SSR, see https://vite-plugin-ssr.com/render-modes for how to modify render() to support SPA
    if (!Page) throw new Error('My render() hook expects pageContext.Page to be defined');

    const pageHtml = ReactDOMServer.renderToString(<Page {...pageProps} />);

    // See https://vite-plugin-ssr.com/head
    const { documentProps } = pageContext.exports;
    const title = (documentProps && documentProps.title) || 'Vite SSR app';
    const desc = (documentProps && documentProps.description) || 'App using Vite + vite-plugin-ssr';

    const documentHtml = escapeInject`<!DOCTYPE html>
    <html lang="en">
      <head>
        <meta charset="UTF-8" />
        <link rel="icon" href="${logoUrl}" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta name="description" content="${desc}" />
        <title>${title}</title>
        <link rel="preconnect" href="https://fonts.googleapis.com"/>
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin/>
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;700&family=Josefin+Sans:wght@400;500;600;700&family=Lato:wght@400;700;900&family=Poppins:wght@400;500;600;700;800;900&family=Roboto:wght@300;400;500;700&display=swap" rel="stylesheet"/>
      </head>
      <body>
        <div id="page-view">${dangerouslySkipEscape(pageHtml)}</div>
      </body>
    </html>`;

    return {
        documentHtml,
        pageContext: {},
    };
}
