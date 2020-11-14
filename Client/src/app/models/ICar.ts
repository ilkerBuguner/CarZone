import { IBrand } from './IBrand';
import { IBrandModel } from './IBrandModel';
import { IComfort } from './IComfort';
import { IExterior } from './IExterior';
import { IProtection } from './IProtection';
import { ISafety } from './ISafety';

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
    safeties?: ISafety[];
    exteriors?: IExterior[];
    protections?: IProtection[];
    comforts?: IComfort[];
}