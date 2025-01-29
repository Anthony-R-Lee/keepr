import { APIItem } from "./APIItem.js";


export class Kept extends APIItem{
  constructor(data){
    super(data)
    this.keepId = data.keepId
    this.vaultId = data.vaultId
  }
}