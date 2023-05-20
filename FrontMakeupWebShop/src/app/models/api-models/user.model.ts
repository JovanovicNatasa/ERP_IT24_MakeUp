export interface User{
ime:string,
prezime:number,
jmbg:string,
email:string,
kontakt:string,
username:string,
lozinka:string,
address:{
  adresaId:number,
  grad: string,
  ulica: string,
  broj: number,
  postanskiBroj: number
}
}
