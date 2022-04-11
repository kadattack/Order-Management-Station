import {OrderProducts} from "./order-products";
import {OrderDetails} from "./order-details";

export interface EmissionRequestDto {

  omsId: string,
  productsDto: Array<OrderProducts>
  orderDetailsDto: OrderDetails
}
