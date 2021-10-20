export interface FilterParamsProduct {
    pagenumber?: number,
    pagesize?: number,
    sort?: string,
    idcategories?: number[],
    minprice?: number,
    maxprice?: number,
    rating?: number[],
    content?: string
}