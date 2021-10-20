import { Product } from "./product.model";

export interface ResponseList {
    totalPage: number,
    data: Product[]
}