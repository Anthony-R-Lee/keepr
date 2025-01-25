import { Profile } from "./Profile.js";

export class Account  extends Profile{
  /**
   * @param {{ id: string; email: string; name: string; picture: string; }} data
   */
  constructor(data) {
    super(data)
    this.email = data.email;
    // TODO add additional properties if needed
  }
}
