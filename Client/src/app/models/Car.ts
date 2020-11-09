import { Brand } from './Brand';
import { BrandModel } from './BrandModel';

export interface Car {
    id?: string;
    price: number;
    fuelType: string;
    horsePower: number;
    year: number;
    bodyType: string;
    brand: Brand
    model: BrandModel;
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