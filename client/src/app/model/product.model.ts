export interface Product{
    id: number,
    name: string,
    description?: string,
    releaseDate: Date,
    discontinuedDate: Date,
    rating: number,
    price: number,
    supplierID: number
}