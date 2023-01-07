
        void Start()
        {
            StartCoroutine(randomHorizontalCurve());
            StartCoroutine(randomVerticalCurve());
        }


        IEnumerator randomHorizontalCurve(){
            // Smoothly change horizontal curve
            while (true){
                yield return new WaitForSeconds(Random.Range(8f,12f));
                if (bendHorizontalSize > 0f) { // 4
                    hSize = Random.Range(-5f, 0f); // -3 
                } else if (bendHorizontalSize < 0f) { // -4
                    hSize = Random.Range(0f, 5f); //3
                } else {
                    hSize = Random.Range(-5f, 5f);
                }
                hSize -= bendHorizontalSize;

                for (float i = 0; i < 6; i += 0.02f){
                    yield return new WaitForSecondsRealtime(0.02f);
                    bendHorizontalSize += hSize/300;
                }

            }
        }

        
        IEnumerator randomVerticalCurve(){
             // Smoothly change vertical curve
            while (true) {
                yield return new WaitForSeconds(Random.Range(10f,20f));
                if (vSize > 0f) {
                    vSize = Random.Range(-2.2f, 0f);
                } else if (hSize < 0f) {
                    vSize = Random.Range(0f, 2.2f);
                } else {
                    vSize = Random.Range(-2.2f, 2.2f);
                }
                vSize -= bendVerticalSize;

                for (float i = 0; i <= 6; i += 0.02f){
                    yield return new WaitForSeconds(0.02f);
                    bendVerticalSize += vSize/300;
                }

            }

        }
        
