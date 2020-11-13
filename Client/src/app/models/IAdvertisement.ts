import { ICar } from "./ICar";
import { IImage } from "./IImage";
import { IUser } from "./IUser";

export interface IAdvertisement {
    id: string;
    title: string;
    description: string;
    views?: number;
    location?: string;
    createdOn?: string;
    car: ICar;
    author?: IUser;
    images?: IImage[];
}