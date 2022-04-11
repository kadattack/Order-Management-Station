import {OrderProducts} from "./order-products";
import {OrderDetails} from "./order-details";
import {EmissionRequestDto} from "./emission-request-dto";

export interface Order {


//   public int Id { get; set; }
// public string omsId { get; set; }
// public string orderId { get; set; }
// public int expectedCompletionTime { get; set; }
// public EmissionRequestDto EmissionRequestDto { get; set; }

  id: number,
  omsId: string,
  orderId: string,
  expectedCompletionTime: number,
  emissionRequestDto : EmissionRequestDto,
}
