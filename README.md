# Azure-NLP
A set of example applications demonstrating how to consume Azure Machine Learning Studio web services.


## Table of contents

1. Azure machine learning sample

     A Console application which eventualy uses a Windows Form for presenting possible usage of n-grams in a text editor as a list of suggested words.

1. SentimentAnalyzer

    A Windows Forms application presenting output of a sentiment analysis service.
    
1. Instruction.docx

    A step-by-step instruction on how to set up required Azure Machine Learning Studio experiments and corresponding web services.
    

## Installation

1. Clone or download solution
1. Build and run Azure Machine Learning Studio experiments according to steps described in Instruction.docx
1. Set up a web service for each experiment
1. For each web service go to "Consume" documentation tab available after clicking on `Test(preview)` link in the table in the `Default Endpoint` section
1. Obtain `Primary Key` or `Secondary Key` and `Request-Response` values and insert them in corresponding project App.config file in the solution
    * `Azure machine learning sample` project corresponds to `N-Gram Extractor` experiment
    * `SentimentAnalyzer` project corresponds to `Sentiment Analyzer` experiment
1. Remember to replace any `&` character in your keys/urls above with the `&amp;`
1. Build and run projects
