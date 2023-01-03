export type IPost = {
    id: string
    author: string
    state: string
    timeStamp: Date
    content: IPostContentSection[]
    replyTo: string
    
}

export type IPostContentSection = {
    contentType: string
    body:string
}