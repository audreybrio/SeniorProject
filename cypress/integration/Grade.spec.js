describe('Grade', function () {

    // User enters in all inforomation 
    it('Calculate Grade', function () {
        // Logs in 
        cy.visit('https://localhost:5002')


        cy.contains('Test').click()

        cy.contains('GPA/Grade Calculator').click()

        cy.contains('Calculate Grade').click()

        // Making selectiovns
        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="assign"]').type('5')
        cy.get('input[id="value"]').type('10')

        // Saves
        cy.contains('Calculate Grade!').click()


    })

    // Missing assignment total choices 
    it('Missing Assignemnt Points', function () {
        cy.visit('https://localhost:5002/gradeCalc')

        // Selections

        // Doesnt save, error message show

        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="value"]').type('10')

        cy.contains('Calculate Grade!').click()

        cy.contains("Error: Missing Information")
    })

    // Missing total points  
    it('Missing Total Points', function () {
        cy.visit('https://localhost:5002/gradeCalc')

        // Selections
        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="assign"]').type('5')

        // Doesnt save, error message shown
        cy.contains('Calculate Grade!').click()

        cy.contains("Error: Missing Information")


    })

    // Saves Grade Correctly  
    it('Saves Grade', function () {
        cy.visit('https://localhost:5002/gradeCalc')

        // Selections
        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="assign"]').type('5')
        cy.get('input[id="value"]').type('10')

        cy.get('input[id="course"]').type('CECS 491B')
        cy.get('input[id="section"]').type('5')

        // Saves
        cy.contains('Save Grade').click()


        cy.get('#gradeId').should('be.visible')


    })

    // Missing Course Name
    it('Missing Course', function () {
        cy.visit('https://localhost:5002/gradeCalc')

        // Selections
        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="assign"]').type('5')
        cy.get('input[id="value"]').type('10')

        cy.get('input[id="section"]').type('5')

        // Saves
        cy.contains('Save Grade').click()

        cy.contains("Error: Missing Course")

    })


    // Missing Section 
    it('Missing Section', function () {
        cy.visit('https://localhost:5002/gradeCalc')

        // Selections
        cy.get('#numAssignments').select(1)

        cy.contains('Enter').click()

        cy.get('input[id="assign"]').type('5')
        cy.get('input[id="value"]').type('10')

        cy.get('input[id="course"]').type('CECS 491B')

        // Saves
        cy.contains('Save Grade').click()

        cy.contains("Error: Missing Section")


    })


})
