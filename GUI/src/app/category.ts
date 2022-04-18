export class Category{
    id: Number;
    name: string;
    isPublic: boolean;
    createdBy:string;
    creationDate:Date;
  
    constructor(name:string='', isPublic:boolean=false, createdBy:string='', createdDate:Date=null) {
      this.name = name;
      this.isPublic = isPublic;    
      this.createdBy = createdBy;
      this.creationDate = createdDate;
    }    
}  