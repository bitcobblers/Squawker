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
            
            <textarea class="w-full" />
            <div class="pt-4">
            {feed.loading && <div>Loading age guess...</div>}
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
