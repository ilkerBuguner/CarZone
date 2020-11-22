import { IComment } from './IComment';
import { IUser } from './IUser';

export interface IReply {
    id: string;
    content: string;
    likes?: number;
    dislikes?: number;
    createdOn?: Date;
    rootComment: IComment;
    author: IUser;
}