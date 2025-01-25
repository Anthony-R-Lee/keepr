import { APIItem } from "./APIItem.js";
import { Profile } from "./Profile.js";

export class Kept extends APIItem{
  constructor(data){
    super(data)
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.kept = data.kept
    this.creatorId = data.creatorId
    this.vaultKeepId = data.vaultKeepId
this.creator = new Profile(data.creator)
  }
}