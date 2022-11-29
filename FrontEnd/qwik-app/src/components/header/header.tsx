import { component$ } from '@builder.io/qwik';
import { QwikLogo } from '../icons/qwik';

export default component$(() => {
  

  return (
    <header>
      <div class="logo">
        <a href="https://localhost:3000/">
          <QwikLogo />
        </a>
      </div>    
    </header>
  );
});
