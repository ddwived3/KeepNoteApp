export class User {
  userId: String;
  name:String;
  contact:String;
  addedDate:Date;

  constructor(userId, name = '', contact = '', addedDate=null) {
    this.userId = userId;
    this.name = name;
    this.contact = contact;
    this.addedDate = addedDate;    
  }
}

