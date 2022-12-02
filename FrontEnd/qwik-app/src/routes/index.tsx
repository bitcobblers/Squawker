import { component$, useResource$, useStore } from '@builder.io/qwik';
import type { DocumentHead } from '@builder.io/qwik-city';
import PostComponent from '../components/post/post-component';
import { IPost } from '../models/post';

export default component$(() => {
    const store = useStore<{ name?: string }>({
        name: undefined,
    });

    const feed = useResource$<IPost[]>(async ({ track, cleanup }) => {
        const abortController = new AbortController();

        track(() => store.name);        
        cleanup(() => abortController.abort('cleanup'));

        const res = await fetch(`https://localhost:3000/api/post`, {
            signal: abortController.signal,
        });

        return res.json();
    });
    store.name = "test";

    return (        
        <div class="mx-auto max-w-xl">                                        
            <div class="flex">
                <textarea class="w-full border-2 rounded-md" />
                <div class="px-2">
                    <button class="bg-sky-900 hover:bg-sky-700 focus:outline-none focus:ring-2 focus:ring-sky-400 focus:ring-offset-2 focus:ring-offset-sky-50 text-white font-semibold h-8 px-6 rounded-lg w-full flex items-center justify-center sm:w-auto dark:bg-sky-500 dark:highlight-white/20 dark:hover:bg-sky-400">Post</button>
                </div>
            </div>
            <div class="pt-4 mt-4 border-t-2">
            {feed.loading && <div>Loading page...</div>}
            {!feed.loading && feed.promise.then((f) =>
                f.map(post => (<PostComponent post={post} />)
                ))}  
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
