//
//  File.swift
//  
//
//  Created by Zed on 12/2/21.
//

import Foundation
import SwiftSoup


func insultSomeone(completion: @escaping(String) -> ()) {
    // call the api and fetch an insult
     let restURL = "https://evilinsult.com/generate_insult.php?lang=en&type=json" // insult api url
     let task = URLSession.shared.dataTask(with: URL(string: restURL)!, completionHandler: { (data, _, err) -> Void in
         if let error = err {
             print(error)
         }
         if let data = data {
            do {
                let json = try JSONSerialization.jsonObject(with: data, options: [])
                if let dick = json as? [String: String] {
                    completion(dick["insult"]!)
                }
            } catch {
                print("!! Error decoding json response")
                completion("The bot has failed it's singular task, please contact the dev")
            }
         }
     })
     task.resume()
 
}


func fetchAdvice(completion: @escaping(String) -> ()) {
    // call the api and fetch an insult
     let restURL = "https://api.adviceslip.com/advice" // insult api url
     let task = URLSession.shared.dataTask(with: URL(string: restURL)!, completionHandler: { (data, _, err) -> Void in
         if let error = err {
             print(error)
         }
         if let data = data {
            print(String(data: data, encoding: .utf8))
            do {
                let json = try JSONSerialization.jsonObject(with: data, options: [])
                print(json)
                if let dick = json as? [String: [String:String]] {
                    print(dick)
                    if let advice = (dick["slip"])?["advice"] {
                        completion(advice)
                    }
                }
            } catch {
                print("!! Error decoding json response")
                completion("The bot has failed it's singular task, please contact the dev")
            }
         }
     })
     task.resume()
}


func fetchTriviaQuestion(completion: @escaping((question: String, answer: String)) -> ()) {
    let restURL = "https://opentdb.com/api.php?amount=1"
//    will replace an occurence of '&quot;' with a quote(')
    func replace(input: String, chars: String) -> String {
        return input.replacingOccurrences(of: "&quot;", with: "'")
    }
    
    
    let task = URLSession.shared.dataTask(with: URL(string: restURL)!, completionHandler: { (data, _, err) -> Void in
        if let error = err {
            print(error)
        }
        if let data = data {
            
           do {
               let json = try JSONSerialization.jsonObject(with: data, options: [])
               if let dick = json as? [String: Any] {
                if let trivia = (dick["results"] as? [[String: Any]])?.first {
                    if let question = trivia["question"] as? String {
                        if let answer = trivia["correct_answer"] as? String {
//                            completion( (question: replace(input: question, chars: "&quot;"), answer: replace(input:answer, chars: "&quot;")) )
                            if let ret = try? (question: Entities.unescape(question), answer: Entities.unescape(answer)) {
                                completion(ret)
                            } else {
                                completion((question:"ERROR: failed to get trivia question, please contact bot dev with this problem", answer:""))
                            }
                            
                        }
                    }
                }
               }
           } catch {
               print("!! Error decoding json response")
//               completion("The bot has failed it's singular task, please contact the dev")
           }
        }
    })
    task.resume()


}

//https://sv443.net/jokeapi/v2/
func fetchJoke(completion: @escaping(String) -> ()) {
    let restURL = "https://v2.jokeapi.dev/joke/Miscellaneous,Dark,Pun,Spooky"
    
    let task = URLSession.shared.dataTask(with: URL(string: restURL)!, completionHandler: { (data, res, err) -> Void in
        if let error = err {
            print(error)
        }
   
        if let data = data {
           do {
               let json = try JSONSerialization.jsonObject(with: data, options: [])
            /*
             hasd a silent error here where the json was not decoding properly because
             there was a section where it was another object in it, so that's why it's being unwrapped to a string
             */
               if let dick = json as? [String: Any] {
                if let setup = dick["setup"] as? String {
                    
                    if let delivery = dick["delivery"] as? String{
                        completion("\(setup)\n\(delivery)")
                    }
                }

                if let joke = dick["joke"] as? String {
                    
                    completion(joke)
                }
                
               }
           } catch {
               print("!! Error decoding json response")
               completion("The bot has failed it's singular task, please contact the dev")
           }
        }
    })
    
    task.resume()
}
