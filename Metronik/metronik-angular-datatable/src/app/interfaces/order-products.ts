import {SerialNumbers} from "./serial-numbers";

export interface OrderProducts {
  id: number,
  gtin: string,
  quantity: number,
  serialNumberType: string,
  serialNumbersDto: Array<SerialNumbers>,
  templateId: string,


}
