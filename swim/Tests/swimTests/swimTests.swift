import XCTest
@testable import swim

final class swimTests: XCTestCase {
    func testExample() {
        // This is an example of a functional test case.
        // Use XCTAssert and related functions to verify your tests produce the correct
        // results.
        XCTAssertEqual(swim().text, "Hello, World!")
    }

    static var allTests = [
        ("testExample", testExample),
    ]
}
