import { APIItem } from "./APIItem.js";
import { Profile } from "./Profile.js";


export class VaultKeep extends APIItem{
constructor(data){
  super(data)
  this.keepId = data.keepId
  this.vaultId = data.vaultId
      this.vaultKeepId = data.vaultKeepId
      this.name = data.name
      this.description = data.description
      this.img = data.img
      this.views = data.views
      this.kept = data.kept
      this.creatorId = data.creatorId
      this.creator = new Profile(data.creator)
}
}