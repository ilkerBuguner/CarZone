import { IBrand } from './IBrand';
import { IBrandModel } from './IBrandModel';

export interface ICar {
    id?: string;
    price: number;
    fuelType: string;
    horsePower: number;
    year: number;
    bodyType: string;
    brand: IBrand
    model: IBrandModel;
    transmission?: string;
    mileage?: string;
    color?: string;
    condition?: string;
    euroStandard?: string;
    doorsCount?: string;
    safeties?: string[];
    exteriors?: string[];
    protections?: string[];
    comforts?: string[];
}