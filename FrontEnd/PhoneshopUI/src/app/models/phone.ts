import Brand from "./brand";

export default interface Phone {
	id: number; 
	brand: Brand;
	description: string;
	type: string; 
	price: number;    
	stock: number;
}
