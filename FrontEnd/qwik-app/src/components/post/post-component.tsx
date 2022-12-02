import { component$ } from '@builder.io/qwik';
import { IPost } from '../../models/post';

export default component$((props: { post: IPost }) => {
    const p = props.post;
    return (
        <div class="border-2 rounded-md mb-2">
            <div class="flex">
                <div class="p-2 w-16">
                    <img class="rounded-full w-10 h-10 mx-auto" src="https://t3.ftcdn.net/jpg/02/53/27/72/360_F_253277232_w0KhD626du0CeTExyY9HV5wANXHRjswV.jpg" />
                </div>
                <div class="p-2">
                    <div>
                        <span class="text-sky-500 font-medium">User Name</span>
                        <span class="ml-4 font">21h</span>
                    </div>
                    <div class="pt-2">{p.content}</div>
                </div>
            </div>
            <div class="flex p-2 border-t flex-row-reverse">                
                <div class="text-blue-300 pr-4">Comments (13)</div>                
            </div>
        </div>
    );
});
