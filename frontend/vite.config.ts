import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  base: './',
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    host: '127.0.0.1',
    port: 80,
    proxy: {
      '/api': 'http://localhost:5027/'
      // '/syncHub': {
      //   target: 'http://localhost:5178/', // Proxy for SignalR hub
      //   ws: true // Important: this enables websocket support
      // }
    }
  },
  build: {
    outDir: './build',
    emptyOutDir: true
  }
});
