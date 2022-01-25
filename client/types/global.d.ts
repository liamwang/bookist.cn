type HttpMethod = 'get' | 'post' | 'put' | 'patch' | 'delete'

interface RequestConfig {
  method?: HttpMethod
  data?: object
  params?: object
}

interface ApiResult<D> {
  code: number
  data: D | undefined
  message: string | undefined
}

interface PagedResult<D> {
  total: number
  items: D[]
  totalPages: number
}
