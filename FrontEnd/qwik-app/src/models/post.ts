export interface IPost {
    id: string
    author: string
    state: string
    timeStamp: Date
    content: IPostContentSection[]
    replyTo: string
    
}

export interface IPostContentSection {
    contentType: string
    body:string
}