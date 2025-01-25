import { APIItem } from "./APIItem.js";
import { Kept } from "./Kept.js";
import { Profile } from "./Profile.js";

export class Keep extends APIItem{
  constructor(data){
super(data)
this.name = data.name
this.description = data.description
this.img = data.img
this.views = data.views
this.creatorId = data.creatorId
this.creator = new Profile(data.creator)
this.kept = new Kept(data.kept)
  }
}