import { component$ } from '@builder.io/qwik';
import { IPost } from '../../models/post';

export default component$((props: { post: IPost }) => {  
    var p = props.post;
    return (
        <div class="border-2 rounded-md"> 
            <div class="mb-2">
                {p.author}
            </div>
            {p.content }
    </div>
  );
});
