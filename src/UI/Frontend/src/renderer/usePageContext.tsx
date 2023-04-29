// `usePageContext` allows us to access `pageContext` in any React component.
// See https://vite-plugin-ssr.com/pageContext-anywhere

import React, { useContext } from 'react';
import { PageContext } from './types';

export { PageContextProvider };
export { usePageContext };

interface PageContextProviderProps {
    pageContext: PageContext;
    children: React.ReactNode;
}

const Context = React.createContext(undefined as any);

function PageContextProvider({ pageContext, children }: PageContextProviderProps) {
    return <Context.Provider value={pageContext}>{children}</Context.Provider>;
}

function usePageContext() {
    const pageContext = useContext(Context);
    return pageContext;
}
