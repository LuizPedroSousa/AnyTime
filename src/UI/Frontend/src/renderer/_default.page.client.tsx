export { render };

import { hydrateRoot } from 'react-dom/client';
import { PageContextClient } from './types';
import { PageContextProvider } from './usePageContext';

import '../styles/globals.css';

export const clientRouting = true;

async function render(pageContext: PageContextClient) {
    const { Page, pageProps } = pageContext;
    console.log(pageProps);
    if (!Page) throw new Error('Client-side render() hook expects pageContext.Page to be defined');
    hydrateRoot(
        document.getElementById('page-view')!,

        <PageContextProvider pageContext={pageContext}>
            <Page {...pageProps} />
        </PageContextProvider>
    );
}
