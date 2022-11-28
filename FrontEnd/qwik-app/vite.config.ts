
import { defineConfig } from 'vite';
import basicSsl from '@vitejs/plugin-basic-ssl'

import { qwikVite } from '@builder.io/qwik/optimizer';
import { qwikCity } from '@builder.io/qwik-city/vite';

import tsconfigPaths from 'vite-tsconfig-paths';

export default defineConfig(() => {
    process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0;
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
            '/api': {
                target: 'https://localhost:7263',
                changeOrigin: true, 
                secure: false, 
                rewrite: (path) => path.replace(/^\/api/, '')                
            }
        },
        
    }
  };
});
