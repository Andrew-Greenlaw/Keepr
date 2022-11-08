export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg || "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png"
    // TODO add additional properties if needed
  }
}
