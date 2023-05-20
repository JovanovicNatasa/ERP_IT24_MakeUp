import { Brand } from "./brand.model"
import { Collection } from "./collection.model"
import { Purpose } from "./purpose.model"
import { Type } from "./type.model"

export interface Product{
  proizvodId: number,
	model: string,
	sastav: string,
	nacinUpotrebe: string,
	cenaPoKom: number,
	kolicinaNaStanju: number,
	brendId: number,
  brend:Brand,
	namenaId: number,
  namena:Purpose,
	tipId: number,
  tip: Type,
	kolekcijaId: number,
  kolekcija: Collection
}
