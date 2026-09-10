import path from 'node:path';
import {defineConfig} from 'vite'
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr'


export default defineConfig(({command}) => ({
    plugins: [react(), svgr()],
    base: command === 'build' ? '/web/' : '/',
    resolve: {
        alias: {
            '@': path.resolve(__dirname, './src'),
        },
    },
    server: {
        host: '127.0.0.1',
        port: 5173
    }
}))
