export interface IPost {
    id: string
    author: string
    state: string
    timeStamp: Date
    content: string
    replyTo: string
    replies: string[]
}