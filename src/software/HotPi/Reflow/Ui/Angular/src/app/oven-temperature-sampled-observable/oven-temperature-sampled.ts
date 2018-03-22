import { Temperature } from './temperature';

export class OvenTemperatureSampled {
  constructor(private _timestamp: Date, private _temperature: Temperature) {
  }

  public get timestamp(): Date {
    return this._timestamp;
  }

  public get temperature(): Temperature {
    return this._temperature;
  }
}
