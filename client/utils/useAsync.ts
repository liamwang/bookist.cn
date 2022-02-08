import { ref } from 'vue'

const useAsync = () => {
  const loading = ref(false)
  const run = async (promise: Promise<any>) => {
    loading.value = true
    try {
      return await promise
    } catch (e) {
      return new Promise(() => {})
    } finally {
      loading.value = false
    }
  }

  return { run, loading }
}

export default useAsync
