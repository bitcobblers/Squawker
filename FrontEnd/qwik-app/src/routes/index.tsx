import { component$, Resource, useResource$, useStore, $ } from '@builder.io/qwik';
import type { DocumentHead } from '@builder.io/qwik-city';
import PostComponent from '../components/post/post-component';
import { IPost } from '../models/post';

export default component$(() => {
    const store = useStore<{ timestamp?: number, content: string }>({
        timestamp: undefined,
        content: ""
    });   

    const feed = useResource$<IPost[]>(async ({ track, cleanup }) => {
        const abortController = new AbortController();

        track(() => store.timestamp);        
        cleanup(() => abortController.abort('cleanup'));

        const res = await fetch(`https://localhost:3000/api/post`, {
            signal: abortController.signal,
        });

        return res.json();
    });

    const createNew = $(() => {
        const abortController = new AbortController();
        if (store.content?.length > 0) {
            const done = fetch(`https://localhost:3000/api/post`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json;"
                },
                body: `"${store.content}"`,
                signal: abortController.signal,
            }).then(res => {
                store.content = "";
                store.timestamp = Date.now();
                console.log(res.json());
            });
        }
    });

    //store.timestamp = Date.now();

    return (        
        <div class="mx-auto max-w-xl">                                        
            <div class="flex">
                <textarea onInput$={(e: Event) => (store.content = (e.target as HTMLInputElement).value)} 
                    class="w-full border-2 rounded-md" />
                <div class="px-2">
                    <button onClick$={createNew}
                        class="bg-sky-900 hover:bg-sky-700 focus:outline-none focus:ring-2 focus:ring-sky-400 focus:ring-offset-2 focus:ring-offset-sky-50 text-white font-semibold h-8 px-6 rounded-lg w-full flex items-center justify-center sm:w-auto dark:bg-sky-500 dark:highlight-white/20 dark:hover:bg-sky-400">
                        Post</button>
                </div>                
            </div>
            <div class="pt-4 mt-4 border-t-2">
                <Resource
                    value={feed}
                    onPending={() => <div>Loading...</div>}
                    onRejected={(reason) => <div>Error: {reason}</div>}
                    onResolved={(data) =>
                        <div>                        
                            {data.map(post => (<PostComponent post={post} />))}
                        </div>
                    }
                />
            </div>
        </div>        
    );
});

export const head: DocumentHead = {
    title: 'Your Socaul Square',
    meta: [
        {
            name: 'description',
            content: 'Open source social media platform.',
        },
    ],
};
