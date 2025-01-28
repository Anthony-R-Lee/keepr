import { APIItem } from "./APIItem.js";


export class VaultKeep extends APIItem{
constructor(data){
  super(data)
  this.keepId = data.keepId
  this.vaultId = data.vaultId
  this.creatorId = data.creatorId
}
}