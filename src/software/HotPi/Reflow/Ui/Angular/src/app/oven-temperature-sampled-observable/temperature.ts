export class Temperature {
  constructor(private _kelvin: number) {
  }

  public get kelvin(): number {
    return this._kelvin;
  }

  public get celsius(): number {
    return this.kelvin - 273.15;
  }
}
