import { Category } from "./category.model";
import { Product } from "./product.model";

export interface FormProduct {
    typeform: number,
    product: Product,
    categories?: Category[]
}