import { component$, Resource, useResource$, useStore, $ } from '@builder.io/qwik';
import type { DocumentHead } from '@builder.io/qwik-city';
import PostComponent from '../../../components/post/post-component';
import { IPost } from '../../../models/post';
import { useLocation } from '@builder.io/qwik-city';


export default component$(() => {

    const store = useStore<{ name?: string, content: string }>({
        name: undefined,
        content: ""
    });
    const location = useLocation();    

    const feed = useResource$<IPost[]>(async ({ track, cleanup }) => {
        const abortController = new AbortController();

        track(() => store.name);        
        cleanup(() => abortController.abort('cleanup'));

        const res = await fetch(`https://localhost:3000/api/t/` + location.params.tag, {
            signal: abortController.signal,
        });

        return res.json();
    });
    store.name = "323";

    return (        
        <div class="mx-auto max-w-xl">                                        
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
