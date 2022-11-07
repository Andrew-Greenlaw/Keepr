export class KeepInVault {
  constructor(data) {
    this.Id = data.Id
    this.CreatedAt = data.CreatedAt
    this.UpdatedAt = data.UpdatedAt
    this.Creator = data.Creator
    this.Name = data.Name
    this.Description = data.Description
    this.Img = data.Img
    this.Views = data.Views
    this.Kept = data.Kept
    this.VaultKeepId = data.VaultKeepId
  }
}