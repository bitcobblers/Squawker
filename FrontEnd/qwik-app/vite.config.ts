
import { defineConfig } from 'vite';
import { qwikVite } from '@builder.io/qwik/optimizer';
import { qwikCity } from '@builder.io/qwik-city/vite';
import tsconfigPaths from 'vite-tsconfig-paths';

import basicSsl from '@vitejs/plugin-basic-ssl'

export default defineConfig(() => {
  return {
      plugins: [qwikCity(), qwikVite(), tsconfigPaths(), basicSsl()],
    preview: {
      headers: {
        'Cache-Control': 'public, max-age=600',
      },
    },
    server: {
        port: 3000,
        https: true,
        strictPort: true,
        proxy: {
            '^/api/.*': {
                target: 'https://localhost:7263',
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, '')
            }
        }         
    }
  };
});
