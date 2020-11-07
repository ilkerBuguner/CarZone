import { Car } from "./Car";
import { Image } from "./Image";
import { User } from "./User";

export interface Advertisement {
    id: string;
    title: string;
    description: string;
    views?: number;
    location?: string;
    createdOn?: string;
    car: Car;
    author?: User;
    images?: Image[];
}