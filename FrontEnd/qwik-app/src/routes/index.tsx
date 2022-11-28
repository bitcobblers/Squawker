import { component$, useResource$, useStore } from '@builder.io/qwik';
import type { DocumentHead } from '@builder.io/qwik-city';
import { Link } from '@builder.io/qwik-city';

export default component$(() => {
    const store = useStore<{ name?: string }>({
        name: undefined,
    });
    const feed = useResource$<{
        content: string;
        age: number;
        count: number;
    }[]>(async ({ track, cleanup }) => {
        console.log("Getting Data");
        track(() => store.name);

        const abortController = new AbortController();
        cleanup(() => abortController.abort('cleanup'));
        const res = await fetch(`https://localhost:3000/api/post`, {
            signal: abortController.signal,
        });

        return res.json();
    });


  return (
      <div>      
          <input onInput$={(e: Event) => (store.name = (e.target as HTMLInputElement).value)} />
    <div>
              {feed.loading && <div>Loading age guess...</div>}
              {!feed.loading &&  (
            <div>
                     Loaded:  {feed.promise.then((f) => (
                    <div>
                        {f[0].content}
                    </div>
                ))}
            </div>
        )}
    </div>
    </div>
  );
});

export const head: DocumentHead = {
  title: 'Your Socaul Square',
  meta: [
    {
      name: 'description',
      content: 'Qwik site description',
    },
  ],
};
