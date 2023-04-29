import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import ssr from 'vite-plugin-ssr/plugin';
import svgr from 'vite-plugin-svgr';

export default defineConfig({
    plugins: [react(), svgr(), ssr()],
});
