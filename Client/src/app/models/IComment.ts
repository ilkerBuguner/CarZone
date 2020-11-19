import { IUser } from './IUser';

export interface IComment {
    id: string;
    content: string;
    likes?: number;
    dislikes?: number;
    createdOn?: Date;
    author: IUser;
}