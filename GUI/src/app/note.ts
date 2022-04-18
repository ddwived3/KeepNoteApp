export class Note {
  id: Number;
  title: string;
  description: string;
  createdBy:string;
  category:string;

  constructor() {
    this.title = '';
    this.description = '';    
    this.createdBy='';
    this.category='';
  }
}